import math


class Vector:

    def __init__(self, x, y, z):
        self.x =x
        self.y = y
        self.z = z

    def add(self, otherVector):
        self.x = self.x + otherVector.x
        self.y = self.y + otherVector.y
        self.z = self.z + otherVector.z
        return self

    def sub(self, otherVector):
        self.x = self.x - otherVector.x
        self.y = self.y - otherVector.y
        self.z = self.z - otherVector.z
        return self

    def div(self, factor):
        self.x = self.x / factor
        self.y = self.y  / factor
        self.z = self.z  / factor
        return self

    def copy(self):
        return Vector(self.x, self.y, self.z)

    def toStr(self):
        return f'x: {self.x} y:{self.y} z:{self.z}'

    def dot(self, otherVector):
        return self.x * otherVector.x + self.y * otherVector.y + self.z * otherVector.z

    def angleBetweenLine(self, otherVector):
        dotProduct = self.dot(otherVector)
        modA = self.length()
        modB = otherVector.length()

        division = dotProduct / (modA * modB)
        return math.acos(division)

    def length(self):
        return math.sqrt(self.x * self.x + self.y * self.y + self.z * self.z)