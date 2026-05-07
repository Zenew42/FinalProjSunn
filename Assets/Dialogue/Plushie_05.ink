INCLUDE Globals.ink
EXTERNAL TableScene()
EXTERNAL HandleObject(id)

//Plush 05 Filly The Pony
{HorseCollected == true: -> RegDia}

"Filly! You found Filly!!"

//plush talking
"Neeeighhh"
    ~plushies += 1
    ~HorseCollected = true
    ~ HandleObject("Horse")
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
