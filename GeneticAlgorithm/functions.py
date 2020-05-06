import math

def diaf1(vct):
    u,w,x,y,z = vct
    return u**2 * w**2 * x * y**2 * z**2 + w * x * z + w**2 * x * y + z + u * w * x**2
def diaf2(vct):
    u,w,x,y,z = vct
    return u * y * z**2 + x * y * z**2 + u * w * x * y**2 * z**2 + z + w * x**2 * y * z**2
def diaf3(vct):
    u,w,x,y,z = vct
    return u * x**2 * y * z + x * y**2 * z + u + u**2 * w * x**2 * y**2 * z**2 + u * w * x**2
