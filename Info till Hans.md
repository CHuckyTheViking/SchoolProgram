# SchoolProgram

Då det inte verkar gå att lägga in "ON CASCADE SET NULL"
i Entity Framework så måste du lägga till det i databasen
när du skapar den.

Det gäller FK för SchoolCLassId i AspNetUsers (Rad 24).

Hemsidan kraschar annars då man ska ta bort en klass med 
lärare/studenter kopplade till den då den inte nollar 
FK i AspNetUsers

Kom tyvärr på att jag inte gjort en readme för detta till dig
innan idag, måndag morgon. Så är tyvärr försent ute men nu vet du... 
