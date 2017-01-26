from PredictWithNN import DrawCardChoice

import sys

chosenAction, value, diff = DrawCardChoice(sys.argv[1])

print chosenAction, ":", value, ":", diff