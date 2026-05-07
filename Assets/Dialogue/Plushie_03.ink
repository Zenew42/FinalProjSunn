INCLUDE Globals.ink
EXTERNAL TableScene()
EXTERNAL HandleObject(id)

//Plushie 03 Mr fluffykins

{CatCollected == true: -> RegDia}

"Wooowie! You found the father cat!"
"Hehehe"

//Plush Talking
"Halloe Mye Namee Ise Mr FluffyKins.. Ande Ie Ame Pregnante"
    ~plushies += 1
    ~CatCollected = true
    ~ HandleObject("Cat")
    {plushies == 6:
        ~TableScene()
         ->END
         - else:
        ->END
    }
    -> END
    
  ===RegDia===
  There's a lot of cat hair on the floor here
  Mr FluffyKins just loves laying in their favourite spot
    ->END
