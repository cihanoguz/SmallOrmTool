# SmallOrmTool
This is written for understand how a orm tool working

* In this project Microsoft.EntityFramework is referanced.
* There exist select,insert,update,delete features with only where condition.

//
* we need some structure for getting tables and columns to use in crud operations.In this projects they(ITableNameProvider,IColumnNameProvider..)
  are designed as a interface implementations because for different kind of sql-provider have different sql-sentences.Our aim is having overrideble 
  structure.It is obviously for different sql providers(such as mssql,oracle sql,...)
