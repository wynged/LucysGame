from keras.models import Sequential
from keras.layers import Dense, Reshape, Flatten
from ReadRecordings import *
from Encode_Decode import encode_one_hot, encode_one_hot_multiple

import numpy as np
import itertools

def get_States_Actions_Results():
    states = []
    actions = []
    results = []
    for j in LoadNewJsonsFromFolder():
        hand, drawn, choice, change = Get_Hand_DrawnCard_Choice_HandChange_Tuple(j) 
        print "HAND | DRAWN | CHOICE | CHANGE"
        print hand, drawn, choice, change 

        hand.append(drawn)
        newState = encode_one_hot_multiple(hand, 12)
        newChoice = encode_one_hot(choice, 4)
        result = change 

        states.append(newState)
        actions.append(newChoice)
        results.append(result)

    return states, actions, results

def TrainExistingModelOnSingleSample(model, singleState, singleChoice, choiceResult):
    guessChoiceVals = model.predict(np.array([singleState]));
    print "Old Guess", guessChoiceVals
    choiceIndex = singleChoice.index(1)
    guessChoiceVals[0][choiceIndex] = choiceResult 

    print "New Values", guessChoiceVals

    model.fit(np.array([singleState]), guessChoiceVals, batch_size=1, nb_epoch=1, verbose=1)


def GetModelForDataShape(States, Choices):
    shapeX = np.array(States[0]).shape
    lenY = len(Choices[0])

    model = Sequential()

    model.add(Dense(64,  input_shape=shapeX, init='lecun_uniform'))
    model.add(Dense(32, activation='linear'))
    model.add(Flatten())
    #model.add(Reshape((32,)))
    model.add(Dense(15, activation='linear'))
    model.add(Dense(output_dim=lenY, activation='linear'))

    model.compile(optimizer='adam',
                  loss='mse')
    return model

def TrainModel(model, States, Choices, Results):
    print "-- {0} -- total training samples".format(len(States))
    i = 0
    for state,choice,result in itertools.izip(States,Choices,Results):
        print "----{0:3} sample".format(i)
        TrainExistingModelOnSingleSample(model, state, choice, result);
        i=i+1

    return model

def SaveModelAndWeights(model):
    # serialize modeVl to JSON
    model_json = model.to_json()
    with open("model\Place_Card_Model.json", "w") as json_file:
        json_file.write(model_json)
    # serialize weights to HDF5
    model.save_weights("model\Place_Card_Model.h5")
    print("Saved model to disk")
    
States, Choices, Results = get_States_Actions_Results()
print "Choices: ", Choices
theModel = GetModelForDataShape(States, Choices)
trainedModel = TrainModel(theModel, States, Choices, Results)
SaveModelAndWeights(trainedModel)