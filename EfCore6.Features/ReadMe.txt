1. When installing Ef core, we can directly install the database provider package like sqlserver. This package will depend on
EF core relational(contains code common to all relation db providers) , which in turn depends on EF core. So we will install all the necessary pacakges by installing just the provider package.

2. Database.EnsureCreated can be used to create the database, Ef core will check if the database exists, if not, then it will
create one by looking at the DBContext class.