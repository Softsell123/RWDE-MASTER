//ClientModel.cs
namespace RWDE
{
    ///<summary>
    ///Represents a model for storing client data.
    ///</summary>
    public class ClientModel
    {
        ///<summary>
        ///Gets or sets the first name of the client.
        ///</summary>
        public string FirstName { get; set; }

        ///<summary>
        ///Gets or sets the last name of the client.
        ///</summary>
        public string LastName { get; set; }

        ///<summary>
        ///Gets or sets the middle initial of the client.
        ///</summary>
        public string MiddleInitial { get; set; }

        ///<summary>
        ///Gets or sets the mother's maiden name of the client.
        ///</summary>
        public string MothersMaidenName { get; set; }

        ///<summary>
        ///Gets or sets the date of birth of the client.
        ///</summary>
        public string DateOfBirth { get; set; }

        ///<summary>
        ///Gets or sets the gender of the client.
        ///</summary>
        public string Gender { get; set; }

        ///<summary>
        ///Gets or sets whether the client is related or affected.
        ///</summary>
        public string IsRelatedOrAffected { get; set; }

        ///<summary>
        ///Gets or sets whether the client's record is shared.
        ///</summary>
        public string RecordIsShared { get; set; }

        ///<summary>
        ///Gets or sets the extended URN of the client.
        ///</summary>
        public string UrnExtended { get; set; }

        ///<summary>
        ///Gets or sets the Aries ID of the client.
        ///</summary>
        public string AriesId { get; set; }

        ///<summary>
        ///Gets or sets the agency client ID of the client.
        ///</summary>
        public string AgencyClientId { get; set; }
    }
}
