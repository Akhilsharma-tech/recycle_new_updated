using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class NoteInputMapperProfile : DomainObjectMapperProfile<Note, NoteInput>
    {
        public NoteInputMapperProfile()
        {
            

            CreateMap<Note, NoteInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Text, o => o.MapFrom(m => m.Text))
                .ForMember(x => x.SelectedAccessLevel, o => o.MapFrom(m => m.AccessLevel))
                .ForMember(x => x.NoteBelongsToId, o => o.Ignore())
                .ForMember(x => x.AccessLevels, o => o.Ignore());

            CreateMap<NoteInput, Note>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Text, o => o.MapFrom(m => m.Text))
                .ForMember(x => x.AccessLevel, o => o.MapFrom(m => m.SelectedAccessLevel));
        }
    }
}