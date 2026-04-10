INCLUDE Globals.ink
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
