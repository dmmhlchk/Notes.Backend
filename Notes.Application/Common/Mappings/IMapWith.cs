using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public interface IMapWith<T>
    {
        //метод который отвечает за создание трансформации из данных в обЪект
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());

    }

}