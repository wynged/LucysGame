from PlaceCardWithNN import PlaceCardChoice

import sys, ast

state = ast.literal_eval(sys.argv[1])
print state

chosenAction, value, diff = PlaceCardChoice(state)

#print chosenAction, ":", value, ":", diff
print chosenAction