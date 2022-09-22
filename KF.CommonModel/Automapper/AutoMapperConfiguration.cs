using AutoMapper;

namespace KF.Common.Model.Automapper
{
    public static class AutoMapperConfiguration
    {
        #region Fields
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        #endregion

        #region Init
        public static void Init()
        {
            try
            {
                mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

                if (mapperConfiguration == null)
                {
                    throw new ArgumentNullException("No configurations");
                }

                mapper = mapperConfiguration.CreateMapper();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        #endregion

        #region Properties
        public static IMapper Mapper => mapper;

        public static MapperConfiguration MapperConfiguration => mapperConfiguration;
        #endregion
    }
}
