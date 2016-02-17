namespace ADGetAllUsers {
    using System.DirectoryServices;
    using System.DirectoryServices.AccountManagement;

    class Program {
        static void Main(string[] args) {
            // Construct context to query your Active Directory
            using (var context = new PrincipalContext(ContextType.Domain, "[YOUR_DOMAIN]")) {
                // Construct UserPrincipal object for this context
                var userPrincipal = new UserPrincipal(context);
                // Search and find every user in the system – PrincipalSearcher instance for what we need!
                using (var searcher = new PrincipalSearcher(userPrincipal)) {
                    var counter = 0u;
                    // Iterate for all users in AD
                    foreach (var result in searcher.FindAll()) {
                        counter++;
                        var de = result.GetUnderlyingObject() as DirectoryEntry;
                        var samAccountName = de.Properties["samAccountName"].Value;
                        System.Console.WriteLine("{0}: {1}", counter, samAccountName);
                    }
                }
            }
        }
    }
}

