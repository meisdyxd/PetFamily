using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared;

public static class Constants
{
    public static class Pet
    {
        public const int MAX_MONIKER_LENGTH = 100;
    }

    public static class TelephoneNumber 
    {
        public const string REGEX = "^[7-8][0-9]{10}$";
    }
    
    public static class Description
    {
        public const int MAX_DESCRIPTION_LENGTH = 1000;
    }

    public static class FullName
    {
        public const int MAX_SURNAME_LENGTH = 255;
        public const int MAX_NAME_LENGTH = 200;
        public const int MAX_PATRONYMIC_LENGTH = 200;
    }

    public static class Requisit 
    {
        public const int MAX_NAME_LENGTH = 32;
        public const int MAX_DESCRIPTION_LENGTH = 300;
        public const int MAX_DETAIL_INSTRUCTION_LENGTH = 600;
    }

    public static class SocialNetwork
    {
        public const int MAX_NAME_LENGTH = 64;
        public const int MAX_LINK_LENGTH = 128;
    }
}
