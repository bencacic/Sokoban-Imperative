# Variables
SRC_DIR := .
BIN_DIR := bin
OUTPUT := $(BIN_DIR)/Sokoban.exe
SRC_FILES := $(wildcard $(SRC_DIR)/*.cs)

# Compiler options
CSC := csc
CSC_FLAGS := /out:$(OUTPUT)

# Targets and rules
.PHONY: all clean

all: $(OUTPUT)

$(OUTPUT): $(SRC_FILES)
	$(CSC) $(CSC_FLAGS) $^

clean:
	rm -f $(OUTPUT)
