INCLUDE Globals.ink
EXTERNAL TableScene()
//Plush 05 Filly The Pony
{HorseCollected == true: -> RegDia}

"Filly! You found Filly!!"

//plush talking
"Neeeighhh"
    ~plushies += 1
    ~HorseCollected = true
    {plushies == 6:
        ~TableScene()
        ->END
       - else:
        ->END
    }
    -> END

  ===RegDia===
The Couch is so big and soft...
I love relaxing in it!
->END
