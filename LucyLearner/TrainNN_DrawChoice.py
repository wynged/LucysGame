from keras.models import Sequential
from keras.layers import Dense
from ReadRecordings import *
from Encode_Decode import encode_one_hot

import numpy as np
import itertools

def get_States_Actions_Results():
    states = []
    actions = []
    results = []
    for j in LoadNewJsonsFromFolder():
        hand, discard, choice, val = Hand_Discard_Choice_CardVal(j) 
        print hand, discard, choice, val 

        newState = encode_one_hot(discard, 12)
        newChoice = encode_one_hot(choice, 1)
        result = val # encode_value(val, -12, 12)

        states.append(newState)
        actions.append(newChoice)
        results.append(result)

    return states, actions, results

def TrainExistingModelOnSingleSample(model, singleState, singleChoice, choiceResult):
    #print "Train Single Sample"
    #print "State: ", singleState
    #print "Choice; ", singleChoice
    #print "Result: ", choiceResult
    guessChoiceVals = model.predict(np.array([singleState]));
    print "Old Guess", guessChoiceVals
    choiceIndex = singleChoice.index(1)
    guessChoiceVals[0][choiceIndex] = choiceResult 

    print "New Values", guessChoiceVals

    model.fit(np.array([singleState]), guessChoiceVals, batch_size=1, nb_epoch=1, verbose=1)


def GetModelForDataShape(States, Choices):
    lenX = len(States[0])
    lenY = len(Choices[0])

    model = Sequential()

    model.add(Dense(64, input_dim=lenX, init='lecun_uniform'))
    model.add(Dense(32, activation='relu'))
    model.add(Dense(lenY, activation='linear'))

    model.compile(optimizer='rmsprop',
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
    with open("model\model.json", "w") as json_file:
        json_file.write(model_json)
    # serialize weights to HDF5
    model.save_weights("model\model.h5")
    print("Saved model to disk")
    
States, Choices, Results = get_States_Actions_Results()
print "Choices: ", Choices
theModel = GetModelForDataShape(States, Choices)
trainedModel = TrainModel(theModel, States, Choices, Results)
SaveModelAndWeights(trainedModel)