If you are careful enough, you will find that in the former solutions we use 'Mysql.Data.EntityFrameworkCore' framework in all microservices. 
In this solution, we keep using 'Mysql.Data.EntityFrameworkCore' framework in IdentityServer but using 'Pomelo.EntityFrameworkCore.MySql'
framework in MoviesService. The 'UseMySQL' method also changed to 'UseMySql'.
