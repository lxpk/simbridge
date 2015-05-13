#pragma strict

function Start () {
	Fib(4);
	for (var i=0;i<100;i++){
		Debug.Log( "Fibonacci 4:" + Fib(i) );
	}
}

function Update () {

}

function Fib (n : int) : int {
    if (!n) {
        return 0;
    } else if (n <= 2) {
        return 1;
    } else {
        return Fib(n - 1) + Fib(n - 2);
    }
}