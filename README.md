Add-on checks values specified in **Word To Group Mapper** lookup and assigns new case (created by email) to a group specified in the lookup.

You can add new group in **Organizational Roles**

Package does not have data binding for any Organizational roles


See **EmailParser.cs** and **CaseEventListener.cs** for details

> **NOTE:**
Magic word check is only performed on an email **body**, excluding email **subject**


> **Word To Group Mapper Lookup**
Must contain coma separated values in Description column

