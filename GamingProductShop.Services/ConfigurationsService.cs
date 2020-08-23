using GamingProductShop.Database;
using GamingProductShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingProductShop.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService Instance
        {
            get
            {
                if (instanse == null) instanse = new ConfigurationsService();
                return instanse;
            }

        }
        private static ConfigurationsService instanse { get; set; }

        private ConfigurationsService()
        {

        }

        #endregion
        public Config GetConfig(string Key)
        {
            using (var context = new GPSContext())
            {
                return context.Configurations.Find(Key);
            }
        }

        public int PageSize()
        {
            using (var context = new GPSContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new GPSContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
