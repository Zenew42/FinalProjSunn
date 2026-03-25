INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 02 Princess Pink Ribbons

{BunnyCollected == true: -> RegDia}

"Yayyy!"
"You found My Bunny!"

//Plush Talking
"Hewo my naime is Princess Pink Ribbons and mai fravorite color is gween!"
//<<<<<<< Updated upstream
~BunnyCollected = true
     ~plushies += 1
    {plushies == 6:
        ~TableScene()
    -else:
        ->END
    }
    ->END
//=======
    
//>>>>>>> Stashed changes

    -> END

    ===RegDia===
    Open the fridge?

    *[yes]
    -> Yes

    *[No]
    -> No

    ===Yes===
    The fridge is empty.
        ->DONE

    ===No===
    -> END
