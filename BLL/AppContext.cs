namespace BLL
{
   public class AppContext
   {
      private static AppContext _instance;
      public static AppContext Instance => _instance ??= new AppContext();
      
      private AppContext() { }
      
      public string UserId { get; set; }
   }
}