using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SportStatistics.Models.ServiceClasses
{
    public class ServiceClasses
    {
        DatabaseContext db = new DatabaseContext();

        public void Add(List<HttpPostedFileBase> files)
        {
            string result = "";
            if (files != null)
            {
                if (files[0] != null)
                {
                    result = new StreamReader(files[0].InputStream).ReadToEnd();
                    AddSportFederation(result);
                }
                if (files[1] != null)
                {                    
                    result = new StreamReader(files[1].InputStream).ReadToEnd();
                    AddFederationSeason(result);
                }
                if (files[2] != null)
                {                    
                    result = new StreamReader(files[2].InputStream).ReadToEnd();
                    AddPlayer(result);
                }
                if (files[3] != null)
                {                    
                    result = new StreamReader(files[3].InputStream).ReadToEnd();
                    AddTeam(result);
                }
                if (files[4] != null)
                {                 
                    result = new StreamReader(files[4].InputStream).ReadToEnd();
                    AddMatch(result);
                }                
            }
        }

        public void AddSportFederation(string data)
        {

        }

        public void AddFederationSeason(string data)
        {

        }

        public void AddPlayer(string data)
        {

        }

        public void AddTeam(string data)
        {

        }

        public void AddMatch(string data)
        {

        }        
    }
}