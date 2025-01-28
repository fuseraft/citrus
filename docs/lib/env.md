# `env`

The `env` package contains functionality for working with environment variables.

## Table of Contents

- [Package Functions](#package-functions)
  - [`all()`](#all)
  - [`argv()`](#argv)
  - [`opt(_key)`](#opt_key)
  - [`get(_varname)`](#get_varname)
  - [`set(_varname, _varvalue)`](#set_varname-_varvalue)
  - [`bin()`](#bin)
  - [`lib()`](#lib)

## Package Functions

### `all()`

Get environment variables as a hashmap.

**Returns**
| Type | Description |
| :--- | :---|
| `Hashmap` | A hashmap representing system environment variables. |

### `argv()`

Get the list of command-line arguments supplied to the program.

**Returns**
| Type | Description |
| :--- | :---|
| `List` | A list of command-line arguments. |

### `opt(_key)`
Get a KVP command-line option value by key.

**Parameters**
| Type | Name | Description |
| :--- | :--- | :--- |
| `String` | `_key` | The option key.|

**Returns**
| Type | Description |
| :--- | :---|
| `String` | The option value. |

## KVP Command-Line Options

You can pass a named command-line argument in the form of a key-value pair.

```bash
hayward -key=value
hayward --key=value
hayward /key=value
```

You can pull these values using this package.

```hayward
println(env::opt("key")) # prints: value
```

### `get(_varname)`

Get an environment variable.

**Parameters**
| Type | Name | Description |
| :--- | :--- | :--- |
| `String` | `_varname` | The environment variable name to retrieve. |

**Returns**
| Type | Description |
| :--- | :---|
| `String` | The environment variable value, an empty string if not found. |

### `set(_varname, _varvalue)`

Set an environment variable.

**Parameters**
| Type | Name | Description |
| :--- | :--- | :--- |
| `String` | `_varname` | The environment variable name to set. |
| `String` | `_varvalue` | The environment variable value. |

**Returns**
| Type | Description |
| :--- | :---|
| `Boolean` | `true` on success. |

### `bin()`

Returns a string containing the path to the Hayward executable.

**Returns**
| Type | Description |
| :--- | :---|
| `String` | Path to Hayward. |

### `lib()`

Returns a string containing the path to the Hayward Standard Library.

**Returns**
| Type | Description |
| :--- | :---|
| `String` | Path to Hayward Standard Library. |