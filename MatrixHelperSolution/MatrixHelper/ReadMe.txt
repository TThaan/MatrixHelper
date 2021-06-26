This library was meant to be a learning tool to show the mathematical processes in a learning/training neural network by means of matrices. 
I guess this wasn't a good very useful idea. Using these matrices in code looks not much different from using plain arrays, at least not with my MatrixExtensionMethods.
And the latter ones are actually way faster than this bloated Matrix library.
I only recommend this to people that don't need to attend to performance and to people that wanna make use of the event handler notifying about changes in a matrix or 
the implemented logger that can show the whole mathematical process and (intermediate) results of a complex algorithm including multiple matrices.
Also an IMatrix is serializable.
At the cost of performance IMatrix involves multiple checks, e.g. if a matrix fulfills the requirements for a certain operation!
