using FluentNHibernate.Conventions;


namespace WebrootUI2.Infrastructure.NHibernateMaps.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            instance.Table(instance.EntityType.Name.InflectTo().Pluralized);
        }
    }
}