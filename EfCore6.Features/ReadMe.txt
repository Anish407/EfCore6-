1. When installing Ef core, we can directly install the database provider package like sqlserver. This package will depend on
EF core relational(contains code common to all relation db providers) , which in turn depends on EF core. So we will install all the necessary pacakges by installing just the provider package.

2. Database.EnsureCreated can be used to create the database, Ef core will check if the database exists, if not, then it will
create one by looking at the DBContext class.

3. Everytime we enumerate thorugh the DBset inside a foreach loop, the connection to the database remain open. So doing a
lot of work inside the loop will result in the database connection to stay open. So its better to run a ToList() and get the 
results in memory before we start iterating through the results.

4. Hardcoded parameters in the LINQ query will be hardcoded in the sql query that is sent to the database, So its better to 
set the value in a parameter and then use the parameter in the LINQ query. Variables are passed as parameters to the generated
SQL query.


