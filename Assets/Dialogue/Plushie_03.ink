INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 03 Mr fluffykins

"Wooowie! You found the father cat!"
"Hehehe"

//Plush Talking
"Halloe Mye Namee Ise Mr FluffyKins.. Ande Ie Ame Pregnante"
    ~plushies += 1
    {plushies == 6:
        ~TableScene()
        else:
        ->END
    }
    -> END
