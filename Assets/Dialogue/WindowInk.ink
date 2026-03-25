INCLUDE Globals.ink
//Window

{free_time == true: 
->WindowAct2
-else: ->WindowAct1
}

//act 1 freetime 1
===WindowAct1===
The weather outside is so nice!! Hehehe
Perfectly Sunny for a perfect birthday
    ->END

//Act 2 freetime 2
===WindowAct2===
It's Storming
    -> END
