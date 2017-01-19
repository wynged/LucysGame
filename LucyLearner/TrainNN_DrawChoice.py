from keras.models import Sequential
from keras.layers import Dense
from ReadRecordings import *
from Encode_Decode import encode_one_hot


def get_x_y():
    x = []
    y = []
    for j in LoadJsonsFromFolder():
        hand, discard, choice, val = Hand_Discard_Choice_CardVal(j) 
        print discard, choice, val 

        newX = encode_one_hot(discard, 12)
        newY = encode_one_hot(val, 12)

        x.append(newX)
        y.append(newY)

    return x, y



X, Y = get_x_y()
lenX = len(X[0])
lenY = len(Y[0])

model = Sequential()

model.add(Dense(64, input_dim=lenX))
model.add(Dense(32))
model.add(Dense(lenY, activation='softmax'))

print lenX

model.compile(optimizer='rmsprop',
              loss='categorical_crossentropy',
              metrics=['accuracy'])

model.fit(X, Y, nb_epoch=2, batch_size=5)



# serialize model to JSON
model_json = model.to_json()
with open("model\model.json", "w") as json_file:
    json_file.write(model_json)
# serialize weights to HDF5
model.save_weights("model\model.h5")
print("Saved model to disk")