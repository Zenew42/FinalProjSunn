INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 02 Princess Pink Ribbons

{BunnyCollected == true: -> RegDia}

"Yayyy!"
"You found My Bunny!"

//Plush Talking
"Hewo my naime is Princess Pink Ribbons and mai fravorite color is gween!"
<<<<<<< Updated upstream
     ~plushies += 1
    {plushies == 6:
        ~TableScene()
         
else:->END}-> END
=======
    ~plushies += 1
    ~BunnyCollected = true
    {plushies == 6:
      ~TableScene()
      
else:-> END }
>>>>>>> Stashed changes

    -> END

    ===RegDia===
    Open the fridge?

    *[yes]
    -> Yes

    *[No]
    -> No

    ===Yes===
    The fridge is empty. 

    ===No===
    -> END
