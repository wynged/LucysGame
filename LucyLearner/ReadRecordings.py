import json
from os import listdir
from os.path import isfile, join


filepath = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\recordedGameplay\\tests - Copy.txt"
DEFAULT_EXPERIENCES_PATH = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\recordedGameplay\\"
DEFAULT_MEMORY_PATH = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\recordedGameplay\\memories"

def LoadNewJsonsFromFolder(aFolderPath = DEFAULT_EXPERIENCES_PATH):
    allfiles = listdir(aFolderPath) 
    #print allfiles

    allJson = []

    for f in allfiles:
        opened = open(aFolderPath+f)
        state = json.loads(opened.readline())
        result = json.loads(opened.readline())

        allJson.append( [state, result] );

    return allJson

def GetVisibleCardValue(card):
    if card['Visibility'] == 1:
        return None
    else:
        return card['Number']

def Hand_Discard_Choice_CardVal(aJson):
    state, results = aJson

    hand = []
    for c in state['PlayerCards']:
        hand.append(GetVisibleCardValue(c))
    
    lastCardVal = state[u'DiscardPile'][-1]['Number']
    drawChoice = results["DrawChoice"]
    drawnCardValue = results["DrawnCardValue"]

    #drawChoice is:
    #   0->Discard
    #   1->MainDeck
    return hand, lastCardVal, drawChoice, drawnCardValue

def GetDrawnCard_Choice_HandChange_Tuple(aJson):
    state, results = aJson
    drawnCard = results["DrawnCardValue"]
    playChoice = results["PlacementChoice"]
    handChange = results["HandValueChange"]

    #PlayChoice is:
    #   0-> V1
    #   1-> V2
    #   2-> H1
    #   3-> H2
    #   4-> Discard
    return drawnCard, playChoice, handChange

#for j in LoadJsonsFromFolder( ):
#    #print j
#    print GetDiscard_Choice_CardVal(j)


    