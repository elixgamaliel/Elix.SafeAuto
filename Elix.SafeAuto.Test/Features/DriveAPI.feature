Feature: Drive API
Test the drive application

@tag API
Scenario: Upload a file
	Given we have a sample file with contents
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is uploaded
	Then the expected output is
	"""
	Alex: 42 miles @ 34 mph
	Dan: 39 miles @ 47 mph
	Bob: 0 miles
	"""

@tag API
Scenario: Upload a file and discard travels
	Given we have a sample file with contents
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	Trip Alex 13:17 14:17 1.0
	Trip Dan 06:33 06:44 150.0
	"""
	When the file is uploaded
	Then the expected output is
	"""
	Alex: 42 miles @ 34 mph
	Dan: 39 miles @ 47 mph
	Bob: 0 miles
	"""
@tag API
Scenario: Invalid command
	Given we have a sample file with contents
	"""
	Driver Dan
	Driver Alex
	Passenger Bob
	Trip Dan 07:15 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is uploaded
	Then the expected result is an error