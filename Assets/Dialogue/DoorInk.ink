INCLUDE Globals.ink
EXTERNAL GoOutsideScene()
//door interaction 

{free_time == true: 
->DoorAct2
-else: ->DoorAct1
}

//Act 1
===DoorAct1===
The party has just started silly you!
you cant leave yet hihihi
    ->END

//Act 2
===DoorAct2===
It's still storming! you'll get all wet if you leave now!
->END


//End of act 2
===Leave===
You stare at the door infront of you...
Step outside?

*[stay]
    ->END
*[freedom?]
    ~GoOutsideScene()
    ->END

===outside===

//need an external function to load the new scene

//glitchy stuff
//cut to black
//swap scene
//pause controller setpause(true)
//friend appears

-> END
