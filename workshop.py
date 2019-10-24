import time


class Car:
    id = None
    x = None
    y = None

data_file = open("smalldata.txt", "r")
ListOfData=[]
g = True
for data in data_file.readlines():
    if g is True:
        g = False

    else:
        arr = data.split(";")
        Car.id = arr[0]
        Car.x = arr[1]
        Car.y = arr[2]
        #print("id = " + Car.id, "x = " + Car.x, "y = " + Car.y)
        ListOfData.append(arr)

data_file.close()

print(len(ListOfData))
print(ListOfData[0])
print(Car[0])
n = 1
#while n < len(ListOfData):
     # print(n)
 #     print(ListOfData[n])
  #    n = n + 1
   #   time.sleep(1)