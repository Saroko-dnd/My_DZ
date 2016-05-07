class Test:
    def __init__(self, NewTestName, NewValues):
        self.TestName = NewTestName
        self.Values = NewValues


class LaboratoryInfo:
    def __init__(self, NewName, NewTests):
        self.LabName = NewName
        self.Tests = NewTests

    def GetTests(self):
        return self.Tests

    def GetLabName(self):
        return self.LabName


def GetNewLab(id, name, telephone):
    Counter = 0
    CurrentTests = []
    # Так можно получать случайные числа
    import random
    while Counter != 3:
        CurrentTestValues = []
        SecondCounter = 0
        while SecondCounter != 5:
            CurrentTestValues.append(random.randint(1, 100))
            SecondCounter += 1
        NewTestName = str(Counter + 1) + " Test"    
        CurrentTests.append(Test(NewTestName, CurrentTestValues))
        Counter += 1
    return LaboratoryInfo("Secret lab", CurrentTests)


