INCLUDE Globals.ink

->main

===main===
Do you want chocolate or vanilla ice-cream?
+[Chocolate]
    ->chosen_flavor("chocolate")
+[Vanilla]
    ->chosen_flavor("vanilla")



===chosen_flavor(chosen)===
~flavor = chosen
You bought {chosen} flavored ice-cream!
Yummy!
->END