#Read Data
import time
class Car:                                #initializing class Car with three objects id, x and y
    id = None
    x = None
    y = None
ListOfData=[]
data_file = open("smalldata.csv", "r")   #declaring new variable with 2 arguments, the file we want to open and the type
                                         #of permission needed. in this case r, which is read from file.
g = True
for data in data_file.readlines():
    if g is True:                        #this variable was used to skip the first line
        g = False
    else:
        arr = data.replace("\n", "").split(";")            #the data is splitet by ;
        Car.id = int(arr[0])                #
        Car.x = float(arr[1])
        Car.y = float(arr[2])
        arr = [int(arr[0]), float(arr[1]), float(arr[2])]
        ListOfData.append(arr)

data_file.close()




def partitionx(ListOfData, low, high):
    i = low
    print(i)
    pivot = ListOfData[high][1]

    for j in range(low, high):
        if ListOfData[j][1] <= pivot:
            i = i+1
            ListOfData[i][1],ListOfData[j][1] = ListOfData[j][1],ListOfData[i][1]
    ListOfData[i][1],ListOfData[high][1] = ListOfData[high][1],ListOfData[i][1]
    return (i+1)

def quicksortx(ListOfData, low, high):
    if low < high:
        pi = partitionx(ListOfData,low,high)
        print("asdaas")
        quicksortx(ListOfData,low,pi+1)
        quicksortx(ListOfData,pi+1,high)


low = 0
high = len(ListOfData)-1
#high = 39999
quicksortx(ListOfData, low, high)
#for i in range(len(ListOfData)-1):
#    print (i,"  ",ListOfData[i][1])
#print(len(ListOfData)-1)

