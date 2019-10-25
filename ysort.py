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


def largex ():
    xmax = ListOfData[0][1]
    for i in range(len(ListOfData)-1):
        if ListOfData[i][1] >= xmax:
           xmax = ListOfData[i][1]       
    print("xmax: ", xmax)
    return xmax

def minix ():
    xmin = ListOfData[0][1]
    for i in range(len(ListOfData)-1):
        if ListOfData[i][1] <= xmin:
           xmin = ListOfData[i][1]
    print("xmin: ", xmin)
    return xmin

    


# Sort y
def sorty(ListOfData):
    n = 10 #amount of partitions
    #xmin = ListOfData[0][1]
    #xmax = ListOfData[len(ListOfData)-1][1]
    rangex = (xmax - xmin)/n #n is number of intervals of range
    a = xmin
    j = 0
    print(xmin, xmax, rangex)
    for i in range(n):
        a = i * rangex + xmin
        b = (1+i) * rangex + xmin
        c = ListOfData[j][1]
        visits = 0
        while c > a and c < b:
            visits = visits + 1
            j = j +1
            c = ListOfData[j][1]
            print(j, "   ", visits, "      ", ListOfData[j][0], ":", c)
        print("          ", j, "   ", visits)
        
xmax = largex()
xmin = minix()
sorty(ListOfData)



#ListOfData
