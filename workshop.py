import time
class Car:                                #initializing class Car with three objects id, x and y
    id = None
    x = None
    y = None
ListOfData=[]
data_file = open("smalldata.txt", "r")   #declaring new variable with 2 arguments, the file we want to open and the type
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
print(ListOfData[1])




























#print("id = " + Car.id, "x = " + Car.x, "y = " + Car.y)
#print(len(ListOfData))

#print(Car[0])
#n = 1
#while n < len(ListOfData):
     # print(n)
 #     print(ListOfData[n])
  #    n = n + 1
   #   time.sleep(1)