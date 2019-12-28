using Autofac;
using MooMed.Module.ProfilePictures.Service;
using MooMed.Module.ProfilePictures.Service.Interface;

namespace MooMed.Module.ProfilePictures
{
    public class ProfilePictureBindings : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ProfilePictureService>()
                .As<IProfilePictureService>()
                .SingleInstance();
        }
    }
}
