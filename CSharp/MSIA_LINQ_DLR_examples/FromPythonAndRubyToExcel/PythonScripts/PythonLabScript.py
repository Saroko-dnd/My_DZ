# -*- coding: utf-8 -*-
class Test:
    def __init__(self, NewTestName, NewValues):
        self.TestName = NewTestName
        self.Values = NewValues

    def GetValues(self):
        return self.Values

    def GetTestName(self):
        return self.TestName


class LaboratoryInfo:
    def __init__(self, NewName, NewTests, NewDatesOfTest, NewValueSeparator, NewValueType):
        self.LabName = NewName
        self.Tests = NewTests
        self.DatesOfTests = NewDatesOfTest
        self.ValueSeparator = NewValueSeparator
        self.ValueType = NewValueType

    def GetTests(self):
        return self.Tests

    def GetLabName(self):
        return self.LabName

    def GetDatesOfTests(self):
        return self.DatesOfTests

    def GetValueSeparator(self):
        return self.ValueSeparator

    def GetValueType(self):
        return self.ValueType


def GetNewLab():
    Counter = 0
    CurrentTests = []
    Dates = [2000, 2001, 2002, 2003, 2004]
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
    return LaboratoryInfo("Секретная лаборатория", CurrentTests, Dates, "Год", "Значение")
