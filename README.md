# Swagger Based Credentials Manager

## Description
This is a simple project which follows Component-Based Software Engineering Architecture. A reusable component is used in this project. The component is written to encrypt and decrypt string using HMACD5. This component an be used in any dotnet based application which have login system or any other sort of data management which needs encryption and decryption. As I have used this component to validate and store credentials (i.e username, password) for different sites in this project.

As the component is created following COM+ model. So the component is a library project which is used in this project by adding the the dll reference of that library project.

The source code of the swagger project is in "DistributedSystemLab_1" this folder and the source code of the component is in "Cryptography" folder.

## How To Run:
This project is based on Dot Net 5.0 framework. So, the prerequistion is that your computer must have net5.0 and related SDK. The easiest way to run:

  * Open DistributedSystemLab_1.sln using Microsoft Visual Studio (preferred 2020 or Above version)
  * In Visual Studio, press run button from the top bar or simply press F5 from the keyboard.
  * A swagger will be opened which consists of three APIs. Those are:
    *  Show_Stored_Credentials ==> HttpGet 
       * Here Stored Credentials will show a list of already stored Credentials.
         i.e (username, password)
    * Store_Credentials ==> HttpPost
      * Using this API one can store Credentials and it will be saved in an encrypted format also. (Actually it acts like registration API)
    * Validate_Credentials ==> HttpPost
      * One can check if his memorized credentials are valid or not by using this API. (Actually it acts like login API)
             
# Component Interface
## Provides Interface:
This Cryptography Component consist of three interface 
  * GetSecretSalt ==> this interface is responsible for generating a random value of character length 10 which will act as the secrett salt in HMACD5 hashing.
  * EncryptSecret ==> this interface is responsible for encryting the plain text using a secret salt. 
  * DecryptAndCheck ==> this interface is responsible for decrypting the stored encrypted text and mathches that text with the plan text supplied by the enduser.

# Conclusion
In a nutshell this is a User Registration and Login related project which uses Data Table to store the information for every Session and uses a component for ensuring the    security of the users credentials (though we also save password in plain text here for testing purpose).
  
  
        

