def encode_one_hot(value, highestNumber):
    vec = [0]*(highestNumber+1)
    vec[value] = 1
    return vec