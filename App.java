// Lab2: Array Manipulation, Functions, Search & Sort Algorithms in Java
// Task Overview:
// Your program will create and manipulate an integer array. The tasks involve generating random values, searching within the array, sorting the array, and organizing code using functions.
// Requirements:
// 1) Array Initialization (max 2 points):
// a. Prompt the user to enter the size of the array (e.g., between 5 and 20
// elements).
// b. Generate an array of the specified size filled with random integers between- 50 and 50.
// c. Display the generated array on the console.
// 2) Search Operation (max 2 points):
// a. Prompt the user to enter an integer value to search within the array.
// b. Implement a function linearSearch(int[] array, int key) that performs a linear search for the value entered by the user.
// c. If the value is found, display the index of the first occurrence. If the value is not found, display a message indicating that it does not exist in the array.
// 3) Sorting Operation (max 3 points):
// a. Implement sorting function:
// sortAscending(int[] array): Sorts the array in ascending order.
// b. Use either bubble sort or selection sort algorithms to complete this task (avoid using built-in sorting functions).
// c. Display the sorted array on the console.
// 4) Average Calculation (max 2 points):
// a. Implement a function calculateAverage (int[] array) that calculates and returns the average value of the array.
// b. Display the average value to the console.
// 5) Submission Guidelines (max 1 point):
// a. Submit the .java file(s) with your code.
// b. Ensure the code compiles and runs without errors.
// c. It is forbidden to share your work with others. It is forbidden to submit work that is not written by you. Work will be graded 0 for rule violations.
import java.util.Scanner;
public class App {
    public static int linearSearch(int[] array, int key) {
        for (int i = 0; i < array.length; i++) {
            if (array[i] == key) {
                return i;
            }
        }
        return -1;
    }
    public static int[] sortAscending(int[] arr) {
        for (int i = 0; i < arr.length - 1; i++) {
            for (int j = 0; j < arr.length - i - 1; j++) {
                if (arr[j] > arr[j + 1]) {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }
    
    public static double calculateAverage(int[] arr) {
        double sum = 0;
        for (int i = 0; i < arr.length; i++) {
            sum += arr[i];
        }
        return sum / arr.length;
    }

    public static void main(String[] args) throws Exception {
        Scanner sc = new Scanner(System.in);
        System.out.print("Enter the size of the array (between 5 and 20): ");
        int size = sc.nextInt();
        if (size < 0 || size > 20) {
            System.out.println("Invalid size. Please enter a size between 5 and 20.");
            sc.close();
            return;
        }
        int[] arr = new int[size];
        for (int i = 0; i < arr.length; i++) {
            arr[i] = (int) (Math.random() * 101) - 50;
        }
        System.out.println("Generated Array:");
        for (int i = 0; i < arr.length; i++) {
            System.out.print(arr[i] + " ");
        }
        System.out.println();
        System.out.print("Enter an integer value to search within the array: ");
        int key = sc.nextInt();
        int index = linearSearch(arr, key);
        if (index != -1) {
            System.out.println("Value found at index: " + index);
        } else {
            System.out.println("Value does not exist in the array.");
        }
        arr = sortAscending(arr);
        System.out.println("Sorted Array:");
        for (int i = 0; i < arr.length; i++) {
            System.out.print(arr[i] + " ");
        }
        System.out.println();
        double average = calculateAverage(arr);
        System.out.println("Average: " + average);
        sc.close();
    }
}
