Feature: Drive Service
Test the drive application

@tag Service
Scenario: Good command list
	Given we have a sample command list
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is processed
	Then the expected result is
	"""
	Alex: 42 miles @ 34 mph
	Dan: 39 miles @ 47 mph
	Bob: 0 miles
	"""
@tag Service
Scenario: Good command list 2
	Given we have a sample command list
	"""
	Driver Elix
	Driver Gamaliel
	Trip Elix 06:01 07:10 17.5
	Trip Gamaliel 05:12 06:42 20.9
	"""
	When the file is processed
	Then the expected result is
	"""
	Gamaliel: 21 miles @ 14 mph
	Elix: 18 miles @ 15 mph
	"""

@tag Service
Scenario: Invalid number of fields for Driver
	Given we have a sample command list
	"""
	Driver Dan 2
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is processed
	Then the expected result is an error with message
	"""
	Invalid number of fields at line 1
	"""
@tag Service
Scenario: Invalid number of fields for Trip
	Given we have a sample command list
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is processed
	Then the expected result is an error with message
	"""
	Invalid number of fields at line 4
	"""

@tag Service
Scenario: Invalid start time
	Given we have a sample command list
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 50:50 07:45 17.3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is processed
	Then the expected result is an error with message
	"""
	Invalid start time at line 4
	"""

@tag Service
Scenario: Invalid miles
	Given we have a sample command list
	"""
	Driver Dan
	Driver Alex
	Driver Bob
	Trip Dan 07:15 07:45 17.A3
	Trip Dan 06:12 06:32 21.8
	Trip Alex 12:01 13:16 42.0
	"""
	When the file is processed
	Then the expected result is an error with message
	"""
	Invalid miles at line 4
	"""