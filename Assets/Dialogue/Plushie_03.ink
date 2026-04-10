INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 03 Mr fluffykins

{CatCollected == true: -> RegDia}

"Wooowie! You found the father cat!"
"Hehehe"

//Plush Talking
"Halloe Mye Namee Ise Mr FluffyKins.. Ande Ie Ame Pregnante"
    ~plushies += 1
    ~CatCollected = true
    {plushies == 6:
        ~TableScene()
         ->END
         - else:
        ->END
    }
    -> END
    
  ===RegDia===
    "Nya, go get the otherss already"
    ->END
