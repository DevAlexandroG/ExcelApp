ExcelApp 📑

Test task in the company. 🏢

Necessaire file here. 
👇👇👇

[Package2.xlsx](https://github.com/DevAlexandroG/ExcelApp/files/11980343/Package2.xlsx)
[Package1.xlsx](https://github.com/DevAlexandroG/ExcelApp/files/11980342/Package1.xlsx)

📓
Test - Implementation of Data Loading from Files with Subsequent Sorting and Merging

There are two Excel files: 
1) "Package 1" 
2) "Package 2"

Both files have five columns:
"ID", "Cargo Name", "Cipher", "Effective Date From", "Effective Date To".

1) If the value in the "Effective Date From" column is empty, it means there is no start date. 
2) If the value in the "Effective Date To" column is empty,it means there is no end date.
3) If both columns have empty values, it means the period is valid indefinitely.

The program should be implemented using Blazor WebAssembly.
It should consist of a single page that includes the following components:

1) A field for uploading two files simultaneously.
  
2) Two fields for specifying dates: "Start Date of the Period"
   and "End Date of the Period". These fields are optional for selection.
   
3) A button to start processing the selection (merging).
   
5) A tabular component to display the result of the merge,
   with the following columns:
   "ID", "Name", "Cipher", "Effective Date From", "Effective Date To", "IsExt", "ExtID".
   
6) After starting the processing of the selection, the program should merge
   the data from both files for the selected period. The data from "Package 1"
   serves as the base. For the data from "Package 1", the value of "IsExt"
   should be set to 0, and for "Package 2", the value of "IsExt" should be set to 1.

The merging process follows these rules:

1) If an item is present in "Package 1" but not in "Package 2", the "ExtID" field is empty.
   
3) If an item is present in both "Package 1" and "Package 2",
   the "ExtID" field is populated with the ID value from "Package 2".
   
5) The "Effective Date From" value is taken as the earliest one, and if it is empty, it is used.
   
7) The "Effective Date To" value is taken as the latest one, and if it is empty, it is used.
   
9) If an item is not present in "Package 1" but exists in "Package 2",
   the "ExtID" field is populated with the ID value from "Package 2", and the "ID" field is empty.

Project screen.
![image](https://github.com/DevAlexandroG/ExcelApp/assets/85547383/400446f0-8123-4003-8779-ec89a0c4a077)
![image](https://github.com/DevAlexandroG/ExcelApp/assets/85547383/cd2b9f47-c1c7-4c79-afa4-af7125ab1996)

