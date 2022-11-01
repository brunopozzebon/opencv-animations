import math

class Utils:

    @staticmethod
    def toRadius(degrees):
        return degrees * (math.pi / 180)

    @staticmethod
    def toDegrees(radius):
        return radius * (180 / math.pi);