 # **TH-Bank - A school group-project.**

< Console Application >
< C# .NET v.8. >


<br>**Code description**<br/>

This application is a mock-up of a banking app. The basic structure is as follows:
Menu objects -> User interface code
DataHandler objects -> Saves and loads information from file in real time
Factory objects -> Handles creation of new objects
Facade objects -> Contains logic that uses several other objects (like making a transaction between accounts)

There are five main abstract classes that objects in the program inherit from:

User objects - Customers, admins
Account objects - Salary account, Savings account
Loan objects - Car loan, House loan
Menu objects - Customer menu, Admin menu
Currency objects - SEK, EUR, USD

There are also two important objects that currently do not inherit from an abstract class,
although this could be expanded on in the future:
Transaction objects
Factory objects

The DataHandler objects inherit from a structure of Interfaces, that varies in functionality
to make sure DataHandler objects only inherit methods they can make use of.
IObjectHandler - The "main" interface that all DataHandlers inherit from. 
IMyDataHandler (inherits from IObjectHandler) - Mostly used by Customer objects to handle data that they own
IAggregateDataHandler (inherits from IObjectHandler) - Mostly used by Admin objects to handle non-user-specific data.

There are also two DataHandlers that differ a bit from the others:
ExchangeDataHandler handles both currencies and exchange rates (this could probably be split up in two in the future).
SystemDataHandler handles system-specific data (for example making sure all new user ID:s are unique).

Finally there are a couple of other classes that handle information:
TransactionSender - corralls transactions and makes sure they are excecuted at the correct time
ExchangeCurrency - contains methods for quickly exchanging money and viewing current exchange rates
FilePaths - static class that contains paths for save files and nothing else
Format - static class that contains methods for number input safety, to be used throughout the program

The facade objects are not implemented yet, but there is a UserFacade class that contains methods for
interacting with user objects.



**Project tool: Asana.**<br/>
(https://app.asana.com/0/1208827563259026/1208803129932156)<br/>

<br>**OOAD.**<br/>
https://eduvarberg.sharepoint.com/:w:/s/SAM-SUT24-SithLords/EdYBKN223ipBi9Gr8NODg4cBTqQrqLv_b7UWlT2gTPQ1hg?e=vdHk76
(link works if you have a Varbergs Kommun-account)

<br>**UML / Flowchart.** Made with draw.Io<br/>
![image](https://github.com/user-attachments/assets/db3f4aba-2587-4146-9657-3789d3955ec3)





 ### **Project members:**
 **Lina:** (https://github.com/LinaOlandersson).<br/>
 **Josefine:** (https://github.com/JosefineNorden).<br/>
 **Gustav:** (https://github.com/GoodStuff15).<br/>
 **Johan:** (https://github.com/JohanHanssonSUT24).<br/>
 **Fredrik:** (https://github.com/JonssonF).
