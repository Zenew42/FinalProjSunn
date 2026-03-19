INCLUDE Globals.ink
EXTERNAL TableScene()
//Plush 05 Filly The Pony

"Filly! You found Filly!!"

//plush talking
"Neeeighhh"
    ~plushies += 1
    {plushies == 6:
        ~TableScene()
       - else:
        ->END
    }
    -> END
