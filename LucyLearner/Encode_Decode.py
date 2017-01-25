def encode_one_hot(value, highestNumber):
    vec = [0]*(highestNumber+1)
    vec[value] = 1
    return vec

def get_choice_val_diff(vec):
    val = vec.max()
    choice = vec.argmax()
    return choice, val, val-vec.argmin()


def scale_value(val, minVal, maxMal):
    return  