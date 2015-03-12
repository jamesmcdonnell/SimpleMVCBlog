using System.Web;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Models;

namespace SimpleBlog
{
    public class Database
    {
            //Represents the key into the Items collection that refers to my open session
            //by using the fully qualified path it ensures we will never have conflicting keys in our session
        private const string SessionKey = "SimpleBlog.Database.SessionKey";
        
        private static ISessionFactory _sessionFactory;

            //used to get the database to expose the session so our controlers can use it
        public static ISession Session
        {
            get { return (ISession) HttpContext.Current.Items[SessionKey]; }
        }
        //called at startup and used to configure nHibernate
        public static void Configure()
        {                    //This is in NHibernate.Cfg
            var config = new Configuration();

                //[1] configure connection string
            config.Configure(); // this overload looks in the web.config file for what we wrote there :-)

                //[2] add our mappings ( with modelMapper )
            var mapper = new ModelMapper();
                    //tell mapper about mappings
            mapper.AddMapping<UserMap>();
                    //tell configuraton about mapper
            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

                //[3]create session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        //called at the beginning of every request and open session
        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        //invoked at the end of every request
        public static void CloseSession()
        {       //try to get the current session
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();
                //Remove the corresponding key from the session dictionary
            HttpContext.Current.Items.Remove(SessionKey);
        }


    }
}