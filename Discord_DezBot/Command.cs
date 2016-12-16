using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_DezBot
{
    class Command
    {
        public string Name;
        public delegate string CmdAction(Message m, string[] args);
        public CmdAction Do;
        public string Description = "No description availible";
        public Plugin Plugin;
        private bool requiresRole;
        public string requiredRole { get; private set; }

        public Command(string name, Plugin pl, CmdAction Do)
        {
            this.Name = name;
            this.Do = Do;
            this.Plugin = pl;
        }

        public Command(string name, string description, Plugin pl, CmdAction Do)
        {
            this.Name = name;
            this.Do = Do;
            this.Description = description;
            this.Plugin = pl;
        }

        public string Run(Message m, string[] args)
        {
            if(requiresRole)
            {
                foreach(Role r in m.User.Roles)
                {
                    if(r.Name == requiredRole)
                        return Do(m, args);
                }
                    return "You don't have permission to do this, sorry :(";
            }
            return Do(m, args);
        }

        public void SetRequiredRole(string role)
        {
            requiredRole = role;
            requiresRole = true;
        }

    }
}
